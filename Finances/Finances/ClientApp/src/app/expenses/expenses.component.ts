import { Component, OnInit } from '@angular/core';
import { CreateExpenseCommand, ExpenseCategoriesListVm, ExpenseCategoryClient, ExpenseCategoryDto, ExpenseClient, ExpenseDto, UpdateExpenseCommand } from '../web-api-client';
import { DatePipe } from '@angular/common';

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
  expenseCategories: ExpenseCategoryDto[] = [];
  updateCommand: UpdateExpenseCommand = new UpdateExpenseCommand();
  isEdit: boolean = false;

  constructor(
    private expensesClient: ExpenseClient,
    private expenseCategoryClient: ExpenseCategoryClient,
  ) { }

  ngOnInit(): void {
    const user = sessionStorage.getItem(`oidc.user:https://localhost:44471:Finances`);
    const parsedUser = JSON.parse(user || '');
    this.userId = parsedUser.profile.sub;
    this.expenseCategoryClient.expenseCategory_GetAll(this.userId).subscribe(result => {
      this.expenseCategories = result.list!;
      this.createCommand.userId = this.userId;
    }, error => console.error(error));

    this.refreshState();
  }

  filterExpenses(month: number, year: number) {
    this.expensesClient.expense_GetAll(month, year, this.userId).subscribe(result => {
      this.expenses = result;
    }, error => console.error(error));
  }

  createExpense() {
    this.expensesClient.expense_Create(this.createCommand).subscribe(result => {
      console.log(result);
      this.createCommand = new CreateExpenseCommand();
      this.createCommand.userId = this.userId;
      this.refreshState();
    }, error => console.error(error));
  }

  editExpense(id: number) {
    const income = this.expenses.find(x => x.id === id);
    this.isEdit = true;
    this.updateCommand.id = id;
    this.updateCommand.merchant = income?.merchant;
    const pipe = new DatePipe('en-US');
    const date = pipe.transform(income?.date, 'yyyy-MM-dd');
    this.updateCommand.date = date!;
    this.updateCommand.total = income?.total;
    this.updateCommand.note = income?.note;
    this.updateCommand.categoryId = this.expenseCategories.find(x => x.name === income?.category)?.id!;
    this.updateCommand.userId = this.userId;
    console.log(this.updateCommand.note);
  }

  updateExpense() {
    this.expensesClient.expense_Update(this.updateCommand).subscribe(result => {
      this.updateCommand = new UpdateExpenseCommand();
      this.isEdit = false;
      this.refreshState();
    }, error => console.error(error));
  }

  deleteExpense(id: number) {
    if(confirm("Are you sure to delete "+id)) {
    this.expensesClient.expense_Delete(id).subscribe(result => {
      this.refreshState();
    }, error => console.error(error));
  }
  }

  refreshState() {
    this.expensesClient.expense_GetAll(this.selectedMonth, this.selectedYear, this.userId).subscribe(result => {
      this.expenses = result!;
    }, error => console.error(error));
  }
}

