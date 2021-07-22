import { Component, Input, OnInit } from '@angular/core';

@Component({
    selector: 'app-loading-indicator-small',
    templateUrl: './loading-indicator-small.component.html',
})
export class LoadingIndicatorSmallComponent implements OnInit {
    @Input() isLoading: boolean;

    constructor() {}
    
    ngOnInit(): void {}
}
