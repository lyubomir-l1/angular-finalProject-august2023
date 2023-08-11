import { Component, OnInit } from '@angular/core';
import { CashflowTypeClient, CashflowTypesVm, CreateExpenseCategoryCommand, CreateIncomeCategoryCommand, ExpenseCategoryClient, ExpenseCategoryDto, IncomeCategoryClient, IncomeCategoryDto } from '../web-api-client';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit {
  expenseCategories: ExpenseCategoryDto[] = [];
  incomeCategories: IncomeCategoryDto[] = [];
  cashflowTypes: CashflowTypesVm[] = [];
  createIncomeCategoryCommand: CreateIncomeCategoryCommand = new CreateIncomeCategoryCommand();
  createExpenseCategoryCommand: CreateExpenseCategoryCommand = new CreateExpenseCategoryCommand();
  userId: string = '';
  incomeCategoryName: string = '';
  incomeCategoryTypeId: number = 0;
  expenseCategoryName: string = '';
  expenseCategoryTypeId: number = 0;

  constructor(
    private incomeCategoryClient: IncomeCategoryClient,
    private expenseCategoryClient: ExpenseCategoryClient,
    private casgflowTypesClient: CashflowTypeClient,
  ) { }

  ngOnInit(): void {
    const user = sessionStorage.getItem(`oidc.user:https://localhost:44471:Finances`);
    const parsedUser = JSON.parse(user || '');
    this.userId = parsedUser.profile.sub;
    this.refreshState();    
  }

  createIncomeCategory() { 
    this.createIncomeCategoryCommand.name = this.incomeCategoryName;
    this.createIncomeCategoryCommand.typeId = this.incomeCategoryTypeId;
    this.createIncomeCategoryCommand.userId = this.userId;
    this.incomeCategoryClient.incomeCategory_Create(this.createIncomeCategoryCommand).subscribe(result => {
      console.log(result);
     
      this.refreshState();
    }, error => console.error(error));
  }

  createExpenseCategory() {
    this.createExpenseCategoryCommand.name = this.expenseCategoryName;
    this.createExpenseCategoryCommand.typeId = this.expenseCategoryTypeId;
    this.expenseCategoryClient.expenseCategory_Create(this.createExpenseCategoryCommand).subscribe(result => {
      console.log(result);
      this.refreshState();
    }, error => console.error(error));
  }

  deleteExpenseCategory(id: number) {
    this.expenseCategoryClient.expenseCategory_Delete(id).subscribe(result => {
      console.log(result);
      this.refreshState();
    }, error => console.error(error));
  }

  deleteIncomeCategory(id: number) {
    this.incomeCategoryClient.incomeCategory_Delete(id).subscribe(result => {
      console.log(result);
      this.refreshState();
    }, error => console.error(error));
  }

  refreshState() {
    this.createIncomeCategoryCommand = new CreateIncomeCategoryCommand();
    this.createIncomeCategoryCommand.userId = this.userId;
    this.createExpenseCategoryCommand = new CreateExpenseCategoryCommand();
    this.createExpenseCategoryCommand.userId = this.userId;

    this.incomeCategoryClient.incomeCategory_GetAll(this.userId).subscribe(result => {
      this.incomeCategories = result.list!;
    }, error => console.error(error));

    this.expenseCategoryClient.expenseCategory_GetAll(this.userId).subscribe(result => {
      this.expenseCategories = result.list!;
    }, error => console.error(error));

    this.casgflowTypesClient.cashflowType_GetAll().subscribe(result => {
      this.cashflowTypes = result!;
    }, error => console.error(error));
  }
}

