import { AuthService } from "./../../../../core/services/auth.service";
import { Component, OnInit, ViewChild } from "@angular/core";
import { Login } from "src/app/core/models/login";
import { FormGroup } from "@angular/forms";
import { Router } from "@angular/router";

@Component({
  selector: "app-login",
  templateUrl: "login.component.html",
})
export class LoginComponent implements OnInit {
  public username: string;
  public password: string;
  public errors: string[];
  public success: boolean;
  public showPassword: boolean;
  public isLoading: boolean;

  @ViewChild("form")
  public form: FormGroup;

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit(): void {}

  onSubmit(): void {
    this.triggerValidation();

    if (!this.form.valid) {
      return;
    }

    var login = new Login({ username: this.username, password: this.password });
    this.isLoading = true;
    this.authService.login(login).subscribe(
      (response) => {
        this.success = true;
        this.errors = [];
        this.isLoading = false;
        console.log(response);
        console.log(this.authService.getCurrentUser());
        setTimeout(() => {
          this.router.navigate(["/"]);
        }, 3000);
      },
      (error) => {
        this.errors = error.error.errors;
        this.isLoading = false;
      }
    );
  }

  private triggerValidation(): void {
    Object.keys(this.form.controls).forEach((key) => {
      this.form.controls[key].markAsTouched();
    });
  }
}
