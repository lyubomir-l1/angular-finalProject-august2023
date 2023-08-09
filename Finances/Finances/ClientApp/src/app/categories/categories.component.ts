import { Component } from '@angular/core';

//import { Income } from '../incomes/incomes.component';
// import { Expense } from '../expenses/expenses.component';



@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent {

  public incomeCategories: IncomeCategory[] = [];

  public expenseCategories: ExpenseCategory[] = [];

}

export interface IncomeCategory {
  Id: number;
  Name: string;
  UserId: string;
  User: string;
  TypeId: number;
 
}

export interface ExpenseCategory {
  Id: number;
  Name: string;
  UserId: string;
  User: string;
  TypeId: number;
  Expenses: string;
}

