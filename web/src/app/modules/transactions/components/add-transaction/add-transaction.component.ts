import { PostTransaction } from 'src/app/core/models/post-transaction';
import { ApiHttpService } from 'src/app/core/services/api-http.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Component, OnInit } from '@angular/core';
import { ApiEndpointsService } from 'src/app/core/services/api-endpoints.service';
import { Category } from 'src/app/core/models/category';
import { finalize, map } from 'rxjs/operators';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import * as moment from 'moment';

@Component({
    selector: 'app-add-transaction',
    templateUrl: './add-transaction.component.html',
})
export class AddTransactionComponent implements OnInit {
    public formGroup: FormGroup;
    public categories: Category[] = [];
    public isLoading: boolean;
    public isExpense: boolean = true;
    public errors: string[];

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
    }

    public get controls() {
        return this.formGroup.controls;
    }

    public load() {
        this.isLoading = true;
        this.apiHttpService
            .get(this.apiEndpointsService.getCategoriesEndpoint())
            .pipe(
                map((data: any) => data.map((x: any) => new Category(x))),
                finalize(() => (this.isLoading = false))
            )
            .subscribe((response) => {
                this.categories = response;
                this.formGroup
                    .get('categoryId')
                    .setValue(
                        this.categories.length ? this.categories[0].id : null
                    );
            });
    }

    public onSubmit() {
        this.formGroup.markAllAsTouched();

        if (this.formGroup.invalid) {
            return;
        }

        this.errors = [];
        this.isLoading = true;
        let endpoint = this.isExpense
            ? this.apiEndpointsService.postExpenseEndpoint()
            : this.apiEndpointsService.postIncomeEndpoint();

        this.apiHttpService
            .post(endpoint, this.formGroup.value as PostTransaction)
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

    public changeType(): void {
        this.isExpense = !this.isExpense;
    }

    private initForm() {
        this.formGroup = this.formBuilder.group({
            amount: [
                0,
                [
                    Validators.required,
                    Validators.min(Number.MIN_SAFE_INTEGER),
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
