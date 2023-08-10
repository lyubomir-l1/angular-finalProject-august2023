import { Component, OnInit } from '@angular/core';
import { ExpenseByCategoryVm, ExpenseCategoryClient, IncomeByCategoryDto, IncomeCategoryClient } from '../web-api-client';

@Component({
  selector: 'app-monthly-report',
  templateUrl: './monthly-report.component.html',
  styleUrls: ['./monthly-report.component.css']
})
export class MonthlyReportComponent implements OnInit {
  selectedMonth: number = new Date().getMonth() + 1;
  selectedYear: number = new Date().getFullYear();
  incomes: IncomeByCategoryDto[] = [];
  expenses: ExpenseByCategoryVm[] = [];
  userId: string = '';
  payToYourself: number = 0.1;
  totalExpenses: number = 0;
  totalIncomes: number = 0;

  constructor(
    private incomeCategoryClient: IncomeCategoryClient,
    private expenseCategoryClient: ExpenseCategoryClient,
  ) { }

  ngOnInit(): void {
    const user = sessionStorage.getItem(`oidc.user:https://localhost:44471:Finances`);
    const parsedUser = JSON.parse(user || '');
    this.userId = parsedUser.profile.sub;
    this.refreshState();
  }

  getMonthlyReport(month: number, year: number) {
    this.selectedMonth = month;
    this.selectedYear = year;
    this.refreshState();
  }

  refreshState() {
    this.incomeCategoryClient.incomeCategory_GetIncomesByCategory(this.selectedMonth, this.selectedYear, this.userId).subscribe(result => {
      this.incomes = result.incomeCategories!;
      this.totalIncomes = result.totals!;
      this.expenseCategoryClient.expenseCategory_GetExpensesByCategory(this.selectedMonth, this.selectedYear, this.userId).subscribe(result => {
        this.expenses = result.expenseCategories!;
        this.totalExpenses = result.totals!;
      }, error => console.error(error));
    }, error => console.error(error));
  }
}



