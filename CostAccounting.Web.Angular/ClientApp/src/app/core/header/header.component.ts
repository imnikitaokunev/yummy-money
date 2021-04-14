import { EnvironmentService } from './../services/environment.service';
import { Component, Input, OnInit } from "@angular/core";

@Component({
    selector: "app-header",
    templateUrl: "header.component.html",
})
export class HeaderComponent implements OnInit {

    public environment: String;
    @Input() data: any;

    constructor(private environmentService: EnvironmentService){}

    ngOnInit(): void {
        this.environmentService.getEnvironment().subscribe((data:string) => {this.environment = data});
    }
}
