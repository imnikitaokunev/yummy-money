import { Component, TemplateRef } from '@angular/core';
import { ToastService } from '../../services/toast.service';

@Component({
    selector: 'app-toasts',
    templateUrl: 'toasts.component.html',
    host: { '[class.ngb-toasts]': 'true' },
})
export class ToastsComponent {
    constructor(public toastService: ToastService) {}

    isTemplate(toast: { textOrTpl: any }) {
        return toast.textOrTpl instanceof TemplateRef;
    }
}
