import { AuthService } from "src/app/core/services/auth.service";
import { EnvironmentService } from "./../services/environment.service";
import { Component, Input, OnInit } from "@angular/core";

@Component({
  selector: "app-header",
  templateUrl: "header.component.html",
})
export class HeaderComponent implements OnInit {
  public environment: String;
  @Input() data: any;

  constructor(
    private environmentService: EnvironmentService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.environmentService.getEnvironment().subscribe((data: string) => {
      this.environment = data;
    });
  }

  public isAuthenticated(): boolean {
    return this.authService.isAuthenticated();
  }

  public logout(): void {
    this.authService.logout();
  }

  public getCurrentUser(): any {
    return this.authService.getCurrentUser();
  }
}
