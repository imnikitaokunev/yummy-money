import { AuthService } from 'src/app/core/services/auth.service';
import { Component, OnInit } from "@angular/core";

@Component({
    selector: "app-home",
    templateUrl: "home.component.html",
})
export class HomeComponent implements OnInit {
    public data: any;

    constructor(private authService: AuthService) {}

    ngOnInit(): void {}

    public isAuthenticated(): boolean {
        return this.authService.isAuthenticated();
    }
}
