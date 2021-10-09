import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
})
export class LoginComponent implements OnInit {
    public showPassword: boolean;
    public formGroup: FormGroup;
    public isLoading: boolean;
    public errors: string[];
    public stayLogged: boolean;

    constructor(private formBuilder: FormBuilder) {}

    ngOnInit(): void {
        this.initForm();
    }

    public get controls() {
        return this.formGroup.controls;
    }

    public onSubmit() {
        this.formGroup.markAllAsTouched();

        if (this.formGroup.invalid) {
            return;
        }

        this.errors = [];
        this.isLoading = true;
    }

    private initForm(): void {
        this.formGroup = this.formBuilder.group({
            username: ['', [Validators.required]],
            password: ['', [Validators.required]],
        });
    }
}
