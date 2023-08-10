import { Component, OnInit } from '@angular/core';
import { ExpenseByYearDto, ExpenseClient, IncomeByYearDto, IncomeClient } from '../web-api-client';

@Component({
  selector: 'app-annual-report',
  templateUrl: './annual-report.component.html',
  styleUrls: ['./annual-report.component.css']
})
export class AnnualReportComponent implements OnInit {
  selectedYear = new Date().getFullYear();
  totalExpenses: number = 0;
  totalIncomes: number = 0;
  userId: string = '';
  incomes: IncomeByYearDto[] = [];
  expenses: ExpenseByYearDto[] = [];
  savings: string = '';
  savingsPer: number = 0;

  constructor(
    private incomesClient: IncomeClient,
    private expensesClient: ExpenseClient,
  ) { }


  ngOnInit(): void {
    const user = sessionStorage.getItem(`oidc.user:https://localhost:44471:Finances`);
    const parsedUser = JSON.parse(user || '');
    this.userId = parsedUser.profile.sub;
    this.refreshState();
  }

  getAnnualReport(year: number) {
    this.selectedYear = year;
    this.refreshState();
  } 

  refreshState() {
    this.incomesClient.income_GetByYear(this.selectedYear, this.userId).subscribe(result => {
      this.incomes = result.incomeSums!;
      this.totalIncomes = result.totals!;  
      this.expensesClient.expense_GetByYear(this.selectedYear, this.userId).subscribe(result => {
        this.expenses = result.expenseSums!;
        this.totalExpenses = result.totals!;
        this.savings = (this.totalIncomes - this.totalExpenses).toFixed(2);
        this.savingsPer = this.totalIncomes === 0 ? 0 : (((this.totalIncomes - this.totalExpenses) / this.totalIncomes) * 100);
      }, error => console.error(error));    
    }, error => console.error(error));    
  }
}

//TODO : Fix totals and savings in filter.
