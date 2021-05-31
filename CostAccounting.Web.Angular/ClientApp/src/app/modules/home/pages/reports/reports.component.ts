import { Component, OnInit } from "@angular/core";
import * as moment from "moment";
import { Moment } from "moment";
import { Label } from "ng2-charts";
import { ExpensesService } from "src/app/core/services/expenses.service";
import { IncomesService } from "src/app/core/services/incomes.service";
import { ChartDataSets, ChartOptions, ChartType } from "chart.js";
import * as pluginDataLabels from "chartjs-plugin-datalabels";
import { Expense } from "src/app/core/models/expense";
import { Income } from "src/app/core/models/income";

@Component({
  selector: "app-home",
  templateUrl: "reports.component.html",
})
export class ReportsComponent implements OnInit {
  public currentDate: Moment;
  public yearExpensesSum: number;
  public yearIncomesSum: number;
  public yearNetSum: number;

  public threeMonthsExpensesSum: number;
  public threeMonthsIncomesSum: number;
  public threeMonthsNetSum: number;
  public monthExpenses: Expense[] = [];
  public monthIncomes: Income[] = [];

  public currentYear: number;
  public years: number[] = [];
  public isYearLoading: boolean;
  public isMonthsLoading: boolean;

  public monthLabels: Label[] = [];

  constructor(
    private expensesService: ExpensesService,
    private incomesService: IncomesService
  ) {}

  ngOnInit(): void {
    this.currentDate = moment();
    let year = this.currentDate.year();
    this.generateYears(year);

    if (this.years && this.years.length) {
      this.currentYear = this.years[0];
    } else {
      this.currentYear = moment().year();
    }

    this.loadYearData(this.currentYear);
    this.loadThreeMonthsData();
    this.generateMonthsLabels(3);
  }

  private loadYearData(year: number): void {
    this.isYearLoading = true;
    this.expensesService
      .getExpenses({
        startDate: moment().year(year).startOf("year").toDate(),
        endDate: moment().year(year).endOf("year").toDate(),
      })
      .subscribe((data) => {
        this.yearExpensesSum = this.getArraySum(data);
        this.incomesService
          .getIncomes({
            startDate: moment().year(year).startOf("year").toDate(),
            endDate: moment().year(year).endOf("year").toDate(),
          })
          .subscribe((data) => {
            this.yearIncomesSum = this.getArraySum(data);
            this.yearNetSum = this.yearIncomesSum - this.yearExpensesSum;
            this.isYearLoading = false;
            // this.getMonthsData(3);
          });
      });
  }

  private loadThreeMonthsData(): void {
    this.isMonthsLoading = true;
    this.expensesService
      .getExpenses({
        startDate: moment()
          .subtract(2, "months")
          .startOf("month")
          .add(3, "hours")
          .toDate(),
        endDate: moment().endOf("month").add(3, "hours").toDate(),
      })
      .subscribe((data) => {
        this.threeMonthsExpensesSum = this.getArraySum(data);
        this.monthExpenses = data;
        this.incomesService
          .getIncomes({
            startDate: moment()
              .subtract(2, "months")
              .startOf("month")
              .add(3, "hours")
              .toDate(),
            endDate: moment().endOf("month").add(3, "hours").toDate(),
          })
          .subscribe((data) => {
            this.monthIncomes = data;
            this.threeMonthsIncomesSum = this.getArraySum(data);
            this.threeMonthsNetSum =
              this.threeMonthsIncomesSum - this.threeMonthsExpensesSum;
            this.isMonthsLoading = false;
            this.barChartData = [
              { data: this.getMonthsExpensesData(3), label: "Expenses" },
              { data: this.getMonthsIncomesData(3), label: "Incomes" },
            ];
          });
      });
  }

  public onYearChanged(event: any): void {
    this.loadYearData(event);
  }

  private getArraySum(array: { amount: number }[]) {
    if (!array) {
      return 0;
    }

    return array.reduce((a, b) => a + b.amount, 0);
  }

  private generateYears(year: number): void {
    for (var i = 0; i < 20; ++i) {
      this.years[i] = year - i;
    }
  }

  public barChartOptions: ChartOptions = {
    responsive: true,
    // We use these empty structures as placeholders for dynamic theming.
    scales: { xAxes: [{}], yAxes: [{}] },
    plugins: {
      datalabels: {
        anchor: "end",
        align: "end",
      },
    },
  };
  public barChartType: ChartType = "bar";
  public barChartLegend = true;
  public barChartPlugins = [pluginDataLabels];

  public barChartData: ChartDataSets[] = [];

  public chartColors: Array<any> = [
    { backgroundColor: "#ff4949" },
    { backgroundColor: "#19cc95" },
  ];

  // events
  public chartClicked({
    event,
    active,
  }: {
    event: MouseEvent;
    active: {}[];
  }): void {
    console.log(event, active);
  }

  public chartHovered({
    event,
    active,
  }: {
    event: MouseEvent;
    active: {}[];
  }): void {
    console.log(event, active);
  }

  public generateMonthsLabels(count: number): void {
    let months: string[] = [];

    for (let i = 0; i < count; ++i) {
      months[i] = moment().subtract(i, "months").format("MMMM");
    }

    this.monthLabels = months.reverse();
  }

  public getMonthsExpensesData(count: number): number[] {
    let months: number[] = [];

    for (let i = 0; i < count; ++i) {
      let date = moment().subtract(i, "months");
      months[i] = this.monthExpenses
        .filter((x) => moment(x.date).isSame(date, "months"))
        .reduce((a, b) => a + b.amount, 0);
    }
    return months.reverse();
  }

  public getMonthsIncomesData(count: number): number[] {
    let months: number[] = [];

    for (let i = 0; i < count; ++i) {
      let date = moment().subtract(i, "months");
      months[i] = this.monthIncomes
        .filter((x) => moment(x.date).isSame(date, "months"))
        .reduce((a, b) => a + b.amount, 0);
    }

    return months.reverse();
  }
}
