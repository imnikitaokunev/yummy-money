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

@Component({
    selector: 'app-calendar',
    templateUrl: './calendar.component.html',
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

    constructor(
        private apiEndpointsService: ApiEndpointsService,
        private apiHttpService: ApiHttpService
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
        this.isLoading = true;

        let request = {
            startDate: this.firstDayOfGrid.toDate(),
            endDate: moment(this.lastDayOfGrid).subtract(1, 'days').toDate(),
            includes: 'Category',
        };

        this.apiHttpService
            .get(this.apiEndpointsService.getExpensesEndpoint(), request)
            .pipe(map((data: any) => data.map((x: any) => new Expense(x))))
            .subscribe((data) => {
                this.expenses = data;
                console.log(data);
                this.apiHttpService
                    .get(this.apiEndpointsService.getIncomesEndpoint(), request)
                    .pipe(
                        map((data: any) => data.map((x: any) => new Income(x)))
                    )
                    .subscribe((data) => {
                        this.incomes = data;
                        console.log(data);
                        this.isLoading = false;
                    });
                //this.expenses = data;
            });

        // this.expensesService
        //     .getExpenses({
        //         startDate: this.firstDayOfGrid.toDate(),
        //         endDate: moment(this.lastDayOfGrid)
        //             .subtract(1, 'days')
        //             .toDate(),
        //     })
        //     .subscribe((data) => {
        //         this.expenses = data;
        //         this.incomesService
        //             .getIncomes({
        //                 startDate: this.firstDayOfGrid.toDate(),
        //                 endDate: moment(this.lastDayOfGrid)
        //                     .subtract(1, 'days')
        //                     .toDate(),
        //             })
        //             .subscribe((data) => {
        //                 this.incomes = data;
        //                 this.isLoading = false;
        //             });
        //     });
    }

    public refresh(): void {
        this.loadData();
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
