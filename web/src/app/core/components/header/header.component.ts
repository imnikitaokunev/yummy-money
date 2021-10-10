import { AuthService } from './../../services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
})
export class HeaderComponent implements OnInit {
    constructor(private authService: AuthService) {}

    public get isLoggedIn(): boolean {
        return this.authService.isLoggedIn;
    }

    public get currentUser(): any {
        return this.authService.currentUser;
    }

    ngOnInit(): void {}

    public signOut(): void {
        this.authService.signOut();
    }
}
