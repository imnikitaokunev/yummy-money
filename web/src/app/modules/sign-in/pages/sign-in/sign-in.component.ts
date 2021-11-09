import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/services/auth.service';
import { finalize } from 'rxjs/operators';
import { SignInRequest } from 'src/app/core/models/sign-in-request';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
    selector: 'app-sign-in',
    templateUrl: './sign-in.component.html',
})
export class SignInComponent implements OnInit {
    public showPassword: boolean;
    public formGroup: FormGroup;
    public isLoading: boolean;
    public errors: string[];
    public success: boolean;

    constructor(
        private formBuilder: FormBuilder,
        private authService: AuthService,
        private router: Router
    ) {
        this.initForm();
    }

    ngOnInit(): void {}

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
        this.formGroup.disable();

        let request = this.formGroup.value as SignInRequest;
        this.authService
            .signIn(request)
            .pipe(
                finalize(() => {
                    this.isLoading = false;
                    this.formGroup.enable();
                })
            )
            .subscribe(
                (response) => {
                    this.success = true;
                    setTimeout(() => {
                        this.router.navigate(['transactions']);
                    }, 3000);
                },
                (error) => {
                    this.errors = error.error.errors.map((x) => x.description);
                }
            );
    }

    private initForm(): void {
        this.formGroup = this.formBuilder.group({
            login: ['', [Validators.required]],
            password: ['', [Validators.required]],
            showPassword: [false],
            stayLogged: [false],
        });
    }
}
