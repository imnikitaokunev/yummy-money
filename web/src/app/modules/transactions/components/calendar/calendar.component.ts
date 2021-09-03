import { AddTransactionComponent } from './../add-transaction/add-transaction.component';
import { Transaction } from './../../../../core/models/transaction';
import { ApiHttpService } from './../../../../core/services/api-http.service';
import { ApiEndpointsService } from './../../../../core/services/api-endpoints.service';
import { Component, OnInit } from '@angular/core';
import * as moment from 'moment';
import { Moment } from 'moment';
import * as range from 'lodash.range';
import { map } from 'rxjs/operators';
import { Expense } from 'src/app/core/models/expense';
import { Income } from 'src/app/core/models/income';
import { DayOfWeek } from '../../models/day-of-week';
import { Sheet } from '../../models/sheet';
import { KeyValue } from '@angular/common';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
    selector: 'app-calendar',
    templateUrl: './calendar.component.html',
})
export class CalendarComponent implements OnInit {
    public weeks: Array<Sheet[]> = [];
    public daysOfWeek = DayOfWeek;
    public transactions: Transaction[] = [];

    public firstDayOfGrid: Moment;
    public lastDayOfGrid: Moment;
    public currentDate: Moment;
    public isLoading: boolean;
    public isError: boolean;

    constructor(
        private apiEndpointsService: ApiEndpointsService,
        private apiHttpService: ApiHttpService,
        private modalService: NgbModal
    ) {}

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
        const firstOfMonth = moment(currentMoment).startOf('month').day();
        const lastOfMonth = moment(currentMoment).endOf('month').day();

        this.firstDayOfGrid = moment(currentMoment)
            .startOf('month')
            .subtract(firstOfMonth, 'days');
        this.lastDayOfGrid = moment(currentMoment)
            .endOf('month')
            .subtract(lastOfMonth, 'days')
            .add(7, 'days');

        const startCalendar = this.firstDayOfGrid.date();

        return range(
            startCalendar,
            startCalendar + this.lastDayOfGrid.diff(this.firstDayOfGrid, 'days')
        ).map((date) => {
            const newDate = moment(this.firstDayOfGrid).date(date);
            return {
                isToday: this.isToday(newDate),
                isCurrentMonth: this.isCurrentMonth(newDate),
                date: newDate.toDate(),
                day: newDate.date(),
            };
        });
    }

    private loadData(): void {
        this.isError = false;
        this.isLoading = true;

        let request = {
            startDate: this.firstDayOfGrid.add(1, 'days').toISOString().slice(0,10),
            endDate: this.lastDayOfGrid.toISOString().slice(0,10),
        };

        this.apiHttpService
            .get(this.apiEndpointsService.getExpensesEndpoint(request))
            .pipe(map((data: any) => data.map((x: any) => new Expense(x))))
            .subscribe(
                (expenses) => {
                    this.apiHttpService
                        .get(
                            this.apiEndpointsService.getIncomesEndpoint(request)
                        )
                        .pipe(
                            map((data: any) =>
                                data.map((x: any) => new Income(x))
                            )
                        )
                        .subscribe(
                            (incomes) => {
                                this.transactions = expenses.concat(incomes);
                                this.isLoading = false;
                                console.log(this.transactions);
                            },
                            (error) => {
                                this.isLoading = false;
                                this.isError = true;
                            }
                        );
                },
                (error) => {
                    this.isLoading = false;
                    this.isError = true;
                }
            );
    }

    public refresh(): void {
        this.loadData();
    }

    public addTransaction(): void {
        this.modalService.open(AddTransactionComponent);
    }

    public isCurrentMonth(currentDate): boolean {
        const today = moment();
        return moment(currentDate).isSame(today, 'months');
    }

    private isToday(date: moment.Moment): boolean {
        return moment().isSame(moment(date), 'day');
    }

    public prevMonth(): void {
        this.currentDate = moment(this.currentDate).subtract(1, 'months');
        this.generateCalendar();
        this.refresh();
    }

    public nextMonth(): void {
        this.currentDate = moment(this.currentDate).add(1, 'months');
        this.generateCalendar();
        this.refresh();
    }

    public getMonthName(offset: number): string {
        return this.currentDate.clone().add(offset, 'months').format('MMMM');
    }

    public getFromDay(array: { date: Date }[], date: Date): any[] {
        if (!array) {
            return [];
        }

        return array.filter((x) => moment(x.date).isSame(date, 'day'));
    }

    public originalOrder = (
        a: KeyValue<string, DayOfWeek>,
        b: KeyValue<string, DayOfWeek>
    ): number => {
        return 0;
    };
}
