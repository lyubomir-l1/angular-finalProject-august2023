import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ExpenseCategory } from '../categories/categories.component';

@Component({
  selector: 'app-expenses',
  templateUrl: './expenses.component.html',
  styleUrls: ['./expenses.component.css']
})
export class ExpensesComponent {
  public expenses: Expense[] = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Expense[]>(baseUrl + 'api/Expenses/GetAll').subscribe(result => {
      this.expenses = result;
    }, error => console.error(error));
  }
}

export interface Expense {
  Id: number;
  Merchant: string;
  Date: Date;
  Note: string;
  Total: number;
  CategoryId: number;
  Category: ExpenseCategory;
  UserId: string;
  User: string;
}

