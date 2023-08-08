import { Component, OnInit } from '@angular/core';
import { CreateExpenseCommand, ExpenseCategoriesListVm, ExpenseCategoryClient, ExpenseClient, ExpenseDto, UpdateExpenseCommand } from '../web-api-client';

@Component({
  selector: 'app-expenses',
  templateUrl: './expenses.component.html',
  styleUrls: ['./expenses.component.css']
})
export class ExpensesComponent implements OnInit {
  userId: string = '';
  selectedMonth = new Date().getMonth() + 1;
  selectedYear = new Date().getFullYear();
  expenses: ExpenseDto[] = [];
  createCommand: CreateExpenseCommand = new CreateExpenseCommand();
  expenseCategories: ExpenseCategoriesListVm[] = [];
  updateCommand: UpdateExpenseCommand = new UpdateExpenseCommand();



  constructor(
    private expensesClient: ExpenseClient, 
    private expenseCategoryClient: ExpenseCategoryClient,
  ) {} 

  ngOnInit(): void {
    const user = sessionStorage.getItem(`oidc.user:https://localhost:44471:Finances`);
    const parsedUser = JSON.parse(user || '');
    this.userId = parsedUser.profile.sub;
    this.expenseCategoryClient.expenseCategory_GetAll(this.userId).subscribe(result => {
    this.expenseCategories = result.list!;
    this.createCommand.userId = this.userId;
      
   }, error => console.error(error));
    

    this.expensesClient.expense_GetAll(this.selectedMonth, this.selectedYear, this.userId).subscribe(result => {
      this.expenses = result.list!;
    }, error => console.error(error));
  }


  filterExpenses(month: number, year: number) {
    this.expensesClient.expense_GetAll(month, year, this.userId).subscribe(result => {
      this.expenses = result.list!;
    }, error => console.error(error));
  }

  createExpenses(){
    this.expensesClient.expense_Create(this.createCommand).subscribe(result => {
      console.log(result);
      this.createCommand = new CreateExpenseCommand();
      this.createCommand.userId = this.userId;
      this.expensesClient.expense_GetAll(this.selectedMonth, this.selectedYear, this.userId).subscribe(result => {
        this.expenses = result.list!;
      }, error => console.error(error));
    }, error => console.error(error));
  }
  deleteExpense(id: number){
    this.expensesClient.expense_Delete(id).subscribe(result => {
      this.expensesClient.expense_GetAll(this.selectedMonth, this.selectedYear, this.userId).subscribe(result => {
        this.expenses = result.list!;
      }, error => console.error(error));
    }, error => console.error(error));
  }

}

