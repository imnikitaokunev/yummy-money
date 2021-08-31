import { environment } from './../../../../../environments/environment';
import { ApiHttpService } from './../../../../core/services/api-http.service';
import { ApiEndpointsService } from './../../../../core/services/api-endpoints.service';
import { Component, OnInit } from '@angular/core';
import { version } from '../../../../../../package.json';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
    public environment = environment;
    public version: string = version;

    constructor() {}

    ngOnInit(): void {}
}
