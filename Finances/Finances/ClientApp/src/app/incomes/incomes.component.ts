import { Component, OnInit } from '@angular/core';
import { IncomeDto, IncomeClient, CreateIncomeCommand, IncomeCategoryClient, IncomeCategoryDto, UpdateIncomeCommand } from '../web-api-client';


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



  constructor(
    private incomesClient: IncomeClient, 
    private incomeCategoryClient: IncomeCategoryClient,
  ) {} 

  ngOnInit(): void {
    const user = sessionStorage.getItem(`oidc.user:https://localhost:44471:Finances`);
    const parsedUser = JSON.parse(user || '');
    this.userId = parsedUser.profile.sub;
    this.incomeCategoryClient.incomeCategory_GetAll(this.userId).subscribe(result => {
    this.incomeCategories = result.list!;
    this.createCommand.userId = this.userId;
      
   }, error => console.error(error));
    

    this.incomesClient.income_GetAll(this.selectedMonth, this.selectedYear, this.userId).subscribe(result => {
      this.incomes = result.list!;
    }, error => console.error(error));
  }


  filterIncomes(month: number, year: number) {
    this.incomesClient.income_GetAll(month, year, this.userId).subscribe(result => {
      this.incomes = result.list!;
    }, error => console.error(error));
  }

  createIncome(){
    this.incomesClient.income_Create(this.createCommand).subscribe(result => {
      console.log(result);
      this.createCommand = new CreateIncomeCommand();
      this.createCommand.userId = this.userId;
      this.incomesClient.income_GetAll(this.selectedMonth, this.selectedYear, this.userId).subscribe(result => {
        this.incomes = result.list!;
      }, error => console.error(error));
    }, error => console.error(error));
  }
  deleteIncome(id: number){
    this.incomesClient.income_Delete(id).subscribe(result => {
      this.incomesClient.income_GetAll(this.selectedMonth, this.selectedYear, this.userId).subscribe(result => {
        this.incomes = result.list!;
      }, error => console.error(error));
    }, error => console.error(error));
  }

}



