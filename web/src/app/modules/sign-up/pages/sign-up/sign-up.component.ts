import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { finalize } from 'rxjs/operators';
import { SignUpRequest } from 'src/app/core/models/sign-up-request';
import { AuthService } from 'src/app/core/services/auth.service';

@Component({
    selector: 'app-sign-up',
    templateUrl: './sign-up.component.html',
})
export class SignUpComponent implements OnInit {
    public showPassword: boolean;
    public showConfirmPassword: boolean;
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

        let request = this.formGroup.value as SignUpRequest;
        this.authService
            .signUp(request)
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
            username: ['', [Validators.required]],
            email: ['', [Validators.required]],
            password: ['', [Validators.required]],
            confirmPassword: ['', [Validators.required]],
            showPassword: [false],
            showConfirmPassword: [false],
        });
    }
}
