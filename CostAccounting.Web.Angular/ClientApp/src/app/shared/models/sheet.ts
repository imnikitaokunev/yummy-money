import { Moment } from "moment";

export interface Sheet {
    day: number;
    date: Date;
    // date: Moment;
    isToday: boolean;
    isCurrentMonth: boolean;
}
