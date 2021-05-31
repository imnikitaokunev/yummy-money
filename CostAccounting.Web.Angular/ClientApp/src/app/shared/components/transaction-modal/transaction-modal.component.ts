import { AuthService } from 'src/app/core/services/auth.service';
import { IncomesService } from './../../../core/services/incomes.service';
import { ExpensesService } from './../../../core/services/expenses.service';
import { CategoriesService } from "./../../../core/services/categories.service";
import { SheetComponent } from "./../sheet/sheet.component";
import { Component, Input, OnChanges, OnInit, SimpleChanges, ViewChild } from "@angular/core";
import { NgbActiveModal, NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { FormGroup } from "@angular/forms";
import { Category } from "src/app/core/models/category";
import { Expense } from 'src/app/core/models/expense';
import { Income } from 'src/app/core/models/income';
import * as moment from 'moment';

@Component({
    selector: "app-transaction-modal",
    templateUrl: "transaction-modal.component.html",
})
export class TransactionModalComponent implements OnInit {
    public data: SheetComponent;
    public amountWithDescription: string;
    public amount: number = 0;
    public expenses: Expense[];
    public incomes: Income[];
    public description: string;
    public category: any;
    public isLoading: boolean = false;
    public categories: Category[] = [];
    public isExpense: boolean = true;
    public type: string;

    @ViewChild("form")
    public form: FormGroup;

    constructor(private activeModal: NgbActiveModal, private categoriesService: CategoriesService, private expensesService: ExpensesService, private incomesService: IncomesService, private authService: AuthService) {}

    ngOnInit() {
        this.isLoading = true;
        this.type = "Expense";
        this.categoriesService.getCategories().subscribe((data) => {
            this.categories = data;
            if (this.categories && this.categories.length) {
                this.category = this.categories[0].id;
            }
            this.isLoading = false;
        });
        this.expenses = this.data.expenses;
        this.incomes = this.data.incomes;
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

    public onTypeChange(event?: any): void {
       console.log(event);
       this.isExpense = event == "Expense";
    }

    public onSubmit(): void {
        this.triggerValidation();

        if (!this.form.valid) {
            return;
        }

        var currentUser = this.authService.getCurrentUser();
        var date = moment(this.data.date).add(3, "hours");
        let data = {amount: this.amount, date: date.toDate(), categoryId: this.category, userId: currentUser.id, description: this.description};

        if(this.isExpense){
            var expense = new Expense (data);
            var result = this.expensesService.postExpense(expense).subscribe(data => console.log(data));
            console.log("add epsenes");
        }
        else {
            var income = new Income (data);
            var result = this.incomesService.postIncome(income).subscribe(data => console.log(data));
            console.log("add incm");

        }

        this.activeModal.close();
    }

    public removeExpense(id: number): void {
        this.expenses = this.expenses.filter(x => x.id !== id);
        this.expensesService.removeExpense(id).subscribe(x => x);
    }

    public removeIncome(id: any): void {
        this.incomes = this.incomes.filter(x => x.id !== id);
        this.incomesService.removeIncome(id).subscribe(x => x);
    }

    private triggerValidation(): void {
        Object.keys(this.form.controls).forEach((key) => {
            this.form.controls[key].markAsTouched();
        });
    }
    
}
