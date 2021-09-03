import { ApiHttpService } from 'src/app/core/services/api-http.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Component, OnInit } from '@angular/core';
import { ApiEndpointsService } from 'src/app/core/services/api-endpoints.service';
import { Category } from 'src/app/core/models/category';
import { map } from 'rxjs/operators';
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

    public load() {
        this.isLoading = true;
        this.apiHttpService
            .get(this.apiEndpointsService.getCategoriesEndpoint())
            .pipe(map((data: any) => data.map((x: any) => new Category(x))))
            .subscribe(
                (response) => {
                    this.categories = response;
                    this.formGroup.get('category').setValue(
                        this.categories.length ? this.categories[0].id : null
                    );
                    this.isLoading = false;
                },
                (error) => {
                    this.isLoading = false;
                }
            );
    }

    public onSubmit() {
        this.formGroup.markAllAsTouched();

        if (this.formGroup.valid) {
            this.isLoading = !this.isLoading;
            console.log(this.formGroup.value);
            // this.activeModal.close();
        }
    }

    private initForm() {
        this.formGroup = this.formBuilder.group({
            amount: [null, [Validators.required, Validators.min(Number.MIN_VALUE), Validators.max(Number.MAX_VALUE)]],
            category: [null, [Validators.required]],
            date: [moment().format('YYYY-MM-DD'), [Validators.required]],
            description: [''],
        });
    }
}
