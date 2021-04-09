import { CategoriesService } from "./../../../core/services/categories.service";
import { SheetComponent } from "./../sheet/sheet.component";
import { Component, Input, OnChanges, OnInit, SimpleChanges, ViewChild } from "@angular/core";
import { NgbActiveModal, NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { FormGroup } from "@angular/forms";
import { Category } from "src/app/core/models/category";

@Component({
    selector: "app-transaction-modal",
    templateUrl: "transaction-modal.component.html",
})
export class TransactionModalComponent implements OnInit {
    public data: SheetComponent;
    public amountWithDescription: string;
    public amount: number = 0;
    public description: string;
    public category: any;
    public isLoading: boolean = false;
    public categories: Category[] = [];

    @ViewChild("form")
    public form: FormGroup;

    constructor(private activeModal: NgbActiveModal, private categoriesService: CategoriesService) {}

    ngOnInit() {
        this.isLoading = true;
        this.categoriesService.getCategories().subscribe((data) => {
            this.categories = data;
            if (this.categories && this.categories.length) {
                this.category = this.categories[0].id;
            }
            this.isLoading = false;
        });
    }

    public close(): void {
        this.activeModal.close();
    }

    public dismiss(): void {
        this.activeModal.dismiss();
    }

    public onChange(amountWithDescription: string): void {
        let values = this.amountWithDescription.split(" ");
        let amount = Number.parseFloat(values[0]);
        let description = values[1];

        if (isNaN(amount)) {
            amount = Number.parseFloat(values[1]);
            description = values[0];

            if (isNaN(amount)) {
                return;
            }
        }

        this.amount = amount;
        this.description = description;
    }

    public onSubmit(): void {
        this.triggerValidation();

        if (!this.form.valid) {
            return;
        }

        console.log("Valid");
    }

    private triggerValidation(): void {
        Object.keys(this.form.controls).forEach((key) => {
            this.form.controls[key].markAsTouched();
        });
    }
}