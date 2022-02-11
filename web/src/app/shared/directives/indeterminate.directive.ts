import { Directive, ElementRef, Input } from '@angular/core';

@Directive({
    selector: '[indeterminate]',
})
export class IndeterminateDirective {
    @Input()
    public set indeterminate(value: any) {
        this.elem.nativeElement.indeterminate = value;
    }

    constructor(private elem: ElementRef) {}
}
