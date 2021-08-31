import { Component, Input, OnInit } from '@angular/core';

@Component({
    selector: 'app-loading-indicator',
    templateUrl: './loading-indicator.component.html'
})
export class LoadingIndicatorComponent implements OnInit {
    @Input() isLoading: boolean;

    constructor() {}

    ngOnInit(): void {}
}
