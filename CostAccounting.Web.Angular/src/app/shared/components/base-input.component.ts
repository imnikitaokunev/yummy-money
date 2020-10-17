import { Input } from "@angular/core";

export class BaseInputComponent {
    @Input() name: string;
    @Input() label: string;
    @Input() model: any;
    @Input() disabled: boolean;
}
