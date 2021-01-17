import { IncomesService } from "./../../../core/services/incomes.service";
import { ExpensesService } from "./../../../core/services/expenses.service";
import { DayOfWeek } from "./../../models/day-of-week";
import { Component, OnInit } from "@angular/core";
import { KeyValue } from "@angular/common";
import { Moment } from "moment";
import { Sheet } from "../../models/sheet";
import * as moment from "moment";
import * as range from "lodash.range";
import { Expense } from "src/app/core/models/expense";
import { Income } from "src/app/core/models/income";

@Component({
    selector: "app-calendar",
    templateUrl: "calendar.component.html",
})
export class CalendarComponent implements OnInit {
    public weeks: Array<Sheet[]> = [];
    public daysOfWeek = DayOfWeek;
    public expenses: Expense[];
    public incomes: Income[];

    public firstDayOfGrid: Moment;
    public lastDayOfGrid: Moment;
    public currentDate: Moment;
    public isLoading: boolean;

    constructor(private expensesService: ExpensesService, private incomesService: IncomesService) {}

    ngOnInit(): void {
        this.currentDate = moment();
        this.generateCalendar();
        this.loadData();
    }

    private generateCalendar(): void {
        const dates = this.fillDates(this.currentDate);
        const weeks: Array<Sheet[]> = [];
        while (dates.length > 0) {
            weeks.push(dates.splice(0, 7));
        }
        this.weeks = weeks;
    }

    private fillDates(currentMoment: moment.Moment): Array<Sheet> {
        const firstOfMonth = moment(currentMoment).startOf("month").day();
        const lastOfMonth = moment(currentMoment).endOf("month").day();

        this.firstDayOfGrid = moment(currentMoment).startOf("month").subtract(firstOfMonth, "days");
        this.lastDayOfGrid = moment(currentMoment).endOf("month").subtract(lastOfMonth, "days").add(7, "days");

        const startCalendar = this.firstDayOfGrid.date();

        return range(startCalendar, startCalendar + this.lastDayOfGrid.diff(this.firstDayOfGrid, "days")).map(
            (date) => {
                const newDate = moment(this.firstDayOfGrid).date(date);
                return {
                    isToday: this.isToday(newDate),
                    isCurrentMonth: this.isCurrentMonth(newDate),
                    date: newDate.toDate(),
                    day: newDate.date(),
                };
            }
        );
    }

    public loadData(): void {
        this.isLoading = true;
        this.expensesService
            .getExpenses({
                startDate: this.firstDayOfGrid.toDate(),
                endDate: this.lastDayOfGrid.subtract(1, "days").toDate(),
            })
            .subscribe((data) => {
                this.expenses = data;
                this.incomesService
                    .getIncomes({
                        startDate: this.firstDayOfGrid.toDate(),
                        endDate: this.lastDayOfGrid.subtract(1, "days").toDate(),
                    })
                    .subscribe((data) => {
                        this.incomes = data;
                        this.isLoading = false;
                    });
            });
    }

    public refresh(): void {
        this.loadData();
    }

    public isCurrentMonth(currentDate): boolean {
        const today = moment();
        return moment(currentDate).isSame(today, "months");
    }

    private isToday(date: moment.Moment): boolean {
        return moment().isSame(moment(date), "day");
    }

    public prevMonth(): void {
        this.currentDate = moment(this.currentDate).subtract(1, "months");
        this.generateCalendar();
        this.refresh();
    }

    public nextMonth(): void {
        this.currentDate = moment(this.currentDate).add(1, "months");
        this.generateCalendar();
        this.refresh();
    }

    public getMonthName(offset: number): string {
        return this.currentDate.clone().add(offset, "months").format("MMMM");
    }

    public getFromDay(array: { date: Date }[], date: Date): any[] {
        return array.filter((x) => moment(x.date).isSame(date, "day"));
    }

    public originalOrder = (a: KeyValue<number, string>, b: KeyValue<number, string>): number => {
        return 0;
    };
}
