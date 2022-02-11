import { environment } from './../../../../../environments/environment';
import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
    public environment = environment;

    constructor() {}

    ngOnInit(): void {}
}
