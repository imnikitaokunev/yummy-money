import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import * as moment from 'moment';
import { finalize, map } from 'rxjs/operators';
import { Category } from 'src/app/core/models/category';
import { PostTransaction } from 'src/app/core/models/post-transaction';
import { Transaction } from 'src/app/core/models/transaction';
import { ApiEndpointsService } from 'src/app/core/services/api-endpoints.service';
import { ApiHttpService } from 'src/app/core/services/api-http.service';

@Component({
    selector: 'app-edit-transaction',
    templateUrl: './edit-transaction.component.html',
})
export class EditTransactionComponent implements OnInit {
    public formGroup: FormGroup;
    public categories: Category[] = [];
    public isLoading: boolean;
    public errors: string[];

    @Input() transaction: Transaction;

    constructor(
        private apiEndpointsService: ApiEndpointsService,
        private apiHttpService: ApiHttpService,
        public activeModal: NgbActiveModal,
        private formBuilder: FormBuilder
    ) {
        this.initForm();
    }

    ngOnInit(): void {
        this.load();
        this.initForm();
        this.applyData();
    }

    public get controls() {
        return this.formGroup.controls;
    }

    public onSubmit() {
        this.formGroup.markAllAsTouched();

        if (this.formGroup.invalid) {
            return;
        }

        this.errors = [];
        this.isLoading = true;
        let request = this.formGroup.value as PostTransaction;

        this.apiHttpService
            .put(
                this.apiEndpointsService.putTransactionEndpoint(request.id),
                request
            )
            .pipe(finalize(() => (this.isLoading = false)))
            .subscribe(
                (response) => {
                    this.activeModal.close();
                },
                (error) => {
                    const validationErrors = error?.error?.errors ?? [];

                    Object.keys(validationErrors).forEach((prop) => {
                        const formControl = this.formGroup.get(prop);
                        if (formControl) {
                            setTimeout(() => {
                                formControl.setErrors({
                                    serverError: validationErrors[prop],
                                });
                            }, 1);
                        }
                    });

                    this.errors = Object.values(validationErrors);
                }
            );
    }

    public load() {
        this.isLoading = true;
        this.apiHttpService
            .get(this.apiEndpointsService.getCategoriesEndpoint())
            .pipe(
                map((data: any) => data.map((x: any) => new Category(x))),
                finalize(() => (this.isLoading = false))
            )
            .subscribe((response) => (this.categories = response));
    }

    private applyData(): void {
        this.formGroup.get('id').setValue(this.transaction.id);
        this.formGroup.get('amount').setValue(this.transaction.amount);
        this.formGroup.get('categoryId').setValue(this.transaction.category.id);
        this.formGroup
            .get('date')
            .setValue(moment(this.transaction.date).format('YYYY-MM-DD'));
        this.formGroup
            .get('description')
            .setValue(this.transaction.description);
    }

    private initForm(): void {
        this.formGroup = this.formBuilder.group({
            id: [0],
            amount: [
                0,
                [
                    Validators.required,
                    Validators.min(Number.EPSILON),
                    Validators.max(Number.MAX_SAFE_INTEGER),
                ],
            ],
            categoryId: [null, [Validators.required]],
            date: [moment().format('YYYY-MM-DD'), [Validators.required]],
            description: ['', [Validators.maxLength(128)]],
            userId: 'f2dce61f-828b-4310-0fd0-08d949626d84',
        });
    }
}
