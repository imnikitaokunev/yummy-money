import { ViewTransactiosComponent } from 'src/app/modules/transactions/components/view-transactios/view-transactios.component';
import { Transaction } from 'src/app/core/models/transaction';
import {
    Component,
    Input,
    OnInit,
    OnChanges,
    SimpleChanges,
    EventEmitter,
    Output,
} from '@angular/core';
import * as moment from 'moment';
import { Moment } from 'moment';
import * as range from 'lodash.range';
import { DayOfWeek } from '../../models/day-of-week';
import { Sheet } from '../../models/sheet';
import { KeyValue } from '@angular/common';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
    selector: 'app-calendar',
    templateUrl: './calendar.component.html',
})
export class CalendarComponent implements OnInit, OnChanges {
    public weeks: Array<Sheet[]> = [];
    public daysOfWeek = DayOfWeek;

    @Input()
    public isLoading: boolean;
    @Input()
    public isError: boolean;
    @Input()
    public currentDate: Moment;
    @Input()
    public firstDay: Moment;
    @Input()
    public lastDay: Moment;
    @Input()
    public transactions: Transaction[] = [];

    @Output() reloaded = new EventEmitter<void>();

    constructor(private modalService: NgbModal) {}

    ngOnInit(): void {
        this.generateCalendar();
    }

    ngOnChanges(changes: SimpleChanges): void {
        if (changes.currentDate) {
            this.generateCalendar();
        }
    }

    public reload(): void {
        this.reloaded.emit();
    }

    private generateCalendar(): void {
        const dates = this.fillDates();
        const weeks: Array<Sheet[]> = [];
        while (dates.length > 0) {
            weeks.push(dates.splice(0, 7));
        }
        this.weeks = weeks;
    }

    private fillDates(): Array<Sheet> {
        const startCalendar = this.firstDay.date();

        return range(
            startCalendar,
            startCalendar + this.lastDay.diff(this.firstDay, 'days')
        ).map((date) => {
            const newDate = moment(this.firstDay).date(date);
            return {
                isToday: this.isToday(newDate),
                isCurrentMonth: this.isCurrentMonth(newDate),
                date: newDate.toDate(),
                day: newDate.date(),
            };
        });
    }

    public viewTransactions(date: Date): void {
        let modal = this.modalService.open(ViewTransactiosComponent);
        modal.componentInstance.date = date;
    }

    public isCurrentMonth(currentDate): boolean {
        const today = moment();
        return moment(currentDate).isSame(today, 'months');
    }

    private isToday(date: moment.Moment): boolean {
        return moment().isSame(moment(date), 'day');
    }

    public getFromDay(array: { date: Date }[], date: Date): any[] {
        return array ? array.filter((x) => moment(x.date).isSame(date, 'day')) : [];
    }

    public originalOrder = (
        a: KeyValue<string, DayOfWeek>,
        b: KeyValue<string, DayOfWeek>
    ): number => {
        return 0;
    };
}
