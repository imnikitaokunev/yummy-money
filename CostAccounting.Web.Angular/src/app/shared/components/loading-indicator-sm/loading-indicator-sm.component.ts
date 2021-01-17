import { Component, Input, OnInit } from "@angular/core";

@Component({
    selector: "app-loading-indicator-sm",
    templateUrl: "loading-indicator-sm.component.html",
})
export class LoadingIndicatorSmComponent implements OnInit {
    @Input() isLoading: boolean;

    constructor() {}

    ngOnInit(): void {}
}
