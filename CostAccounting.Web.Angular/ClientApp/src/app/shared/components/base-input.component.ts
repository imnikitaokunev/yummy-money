import { Input, Directive } from "@angular/core";

@Directive()
export class BaseInputComponent {
    @Input() name: string;
    @Input() label: string;
    @Input() labelContent: string;
    @Input() model: any;
    @Input() disabled: boolean;
    @Input() required: boolean;
}
