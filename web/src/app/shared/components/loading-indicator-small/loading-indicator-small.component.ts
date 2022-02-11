import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
    selector: 'app-loading-indicator-small',
    templateUrl: './loading-indicator-small.component.html',
})
export class LoadingIndicatorSmallComponent implements OnInit {
    @Input() isLoading: boolean;
    @Input() isError: boolean;

    @Output() reloaded = new EventEmitter<void>();

    constructor() {}

    ngOnInit(): void {}

    public reload(): void {
        this.reloaded.emit();
    }
}
