import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CategoriesComponent, IncomeCategory } from '../categories/categories.component'

@Component({
  selector: 'app-incomes',
  templateUrl: './incomes.component.html',
  styleUrls: ['./incomes.component.css']
})
export class IncomesComponent {
  public incomes: Income[] = [];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Income[]>(baseUrl + 'api/Income/GetAll').subscribe(result => {
      this.incomes = result;
    }, error => console.error(error));
  }
}

export interface Income {
  Id: number;
  Merchant: string;
  Date: Date;
  Note: string;
  Total: number;
  CategoryId: number;
  Category: IncomeCategory;
  UserId: string;
  User: string;
}
