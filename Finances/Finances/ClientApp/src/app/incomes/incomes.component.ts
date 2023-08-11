import { Component, OnInit } from '@angular/core';
import { IncomeDto, IncomeClient, CreateIncomeCommand, IncomeCategoryClient, IncomeCategoryDto, UpdateIncomeCommand } from '../web-api-client';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-incomes',
  templateUrl: './incomes.component.html',
  styleUrls: ['./incomes.component.css']
})
export class IncomesComponent implements OnInit {
  userId: string = '';
  selectedMonth = new Date().getMonth() + 1;
  selectedYear = new Date().getFullYear();
  incomes: IncomeDto[] = [];
  createCommand: CreateIncomeCommand = new CreateIncomeCommand();
  incomeCategories: IncomeCategoryDto[] = [];
  updateCommand: UpdateIncomeCommand = new UpdateIncomeCommand();
  merchantValidation: boolean = false;
  isEdit = false;

  constructor(
    private incomesClient: IncomeClient,
    private incomeCategoryClient: IncomeCategoryClient,
  ) { }

  ngOnInit(): void {
    const user = sessionStorage.getItem(`oidc.user:https://localhost:44471:Finances`);
    const parsedUser = JSON.parse(user || '');
    this.userId = parsedUser.profile.sub;
    this.incomeCategoryClient.incomeCategory_GetAll(this.userId).subscribe(result => {
      this.incomeCategories = result.list!;
      this.createCommand.userId = this.userId;

    }, error => console.error(error));

    this.refreshState();
  }

  filterIncomes(month: number, year: number) {
    this.selectedYear = year;
    this.selectedMonth = month;
    this.refreshState();
  }

  
  createIncome() {
    this.incomesClient.income_Create(this.createCommand).subscribe(result => {
      console.log(result);
      this.createCommand = new CreateIncomeCommand();
      this.createCommand.userId = this.userId;
      this.refreshState();
    }, error => console.error(error));
  }

  editIncome(id: number) {
    const income = this.incomes.find(x => x.id === id);
    this.isEdit = true;
    this.updateCommand.id = id;
    this.updateCommand.merchant = income?.merchant;
    const pipe = new DatePipe('en-US');
    const date = pipe.transform(income?.date, 'yyyy-MM-dd');
    this.updateCommand.date = date!;
    this.updateCommand.total = income?.total;
    this.updateCommand.note = income?.note;
    this.updateCommand.categoryId = this.incomeCategories.find(x => x.name === income?.category)?.id!;
    this.updateCommand.userId = this.userId;
  }

  updateIncome() {
    this.incomesClient.income_Update(this.updateCommand).subscribe(result => {    
      this.refreshState();
    }, error => console.error(error));
  }


  deleteIncome(id: number) {
    if(confirm("Are you sure to delete "+id)) {
      this.incomesClient.income_Delete(id).subscribe(result => {
        this.refreshState();
      }, error => console.error(error));
    }
  }

  refreshState() {
    this.incomesClient.income_GetAll(this.selectedMonth, this.selectedYear, this.userId).subscribe(result => {
      this.incomes = result.list!;
    }, error => console.error(error));
    this.updateCommand = new UpdateIncomeCommand();
    this.isEdit = false;
  }
}



