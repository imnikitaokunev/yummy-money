import { Component, OnInit, ViewChild } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { Router } from "@angular/router";
import { Register } from "src/app/core/models/register";
import { AuthService } from "src/app/core/services/auth.service";

@Component({
  selector: "app-register",
  templateUrl: "register.component.html",
})
export class RegisterComponent implements OnInit {
  public username: string;
  public password: string;
  public email: string;
  public firstName: string;
  public lastName: string;
  public errors: string[];
  public success: boolean;
  public showPassword: boolean;

  @ViewChild("form")
  public form: FormGroup;

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit(): void {}

  onSubmit(): void {
    this.triggerValidation();

    if (!this.form.valid) {
      return;
    }

    this.errors = [];
    var register = new Register({
      username: this.username,
      password: this.password,
      email: this.email,
      firstName: this.firstName,
      lastName: this.lastName,
    });

    this.authService.register(register).subscribe(
      (response) => {
        this.success = true;
        setTimeout(() => {
          this.router.navigate(["/login"]);
        }, 5000);
      },
      (error) => {
        console.log(error);
        this.errors = Object.keys(error.error.errors).map(function (key) {
          var value = error.error.errors[key];
          return `${key}: ${value}`;
        });
      }
    );
  }

  private triggerValidation(): void {
    Object.keys(this.form.controls).forEach((key) => {
      this.form.controls[key].markAsTouched();
    });
  }
}
