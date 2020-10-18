import { Pipe, PipeTransform } from "@angular/core";

@Pipe({
    name: "negative",
})
export class NegativePipe implements PipeTransform {
    transform(value: number, ...args: any[]): string {
        return `-${value}`;
    }
}
