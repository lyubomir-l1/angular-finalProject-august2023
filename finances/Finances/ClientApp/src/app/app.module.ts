import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { IncomesComponent } from './incomes/incomes.component';
import { ExpensesComponent } from './expenses/expenses.component';
import { CategoriesComponent } from './categories/categories.component';
import { MonthlyReportComponent } from './monthly-report/monthly-report.component';
import { AnnualReportComponent } from './annual-report/annual-report.component';
import { LoanCalculatorComponent } from './loan-calculator/loan-calculator.component';
import { FilterComponent } from './shared/filter/filter.component';
import { InputComponent } from './shared/input/input.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    IncomesComponent,
    ExpensesComponent,
    CategoriesComponent,
    MonthlyReportComponent,
    AnnualReportComponent,
    LoanCalculatorComponent,
    FilterComponent,
    InputComponent,
    
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent, canActivate: [AuthorizeGuard] },
      { path: 'incomes', component: IncomesComponent },
      { path: 'expenses', component: ExpensesComponent },
      { path: 'categories', component: CategoriesComponent },
      { path: 'monthly-report', component: MonthlyReportComponent },
      { path: 'anual-report', component: AnnualReportComponent },
      { path: 'loan-calculator', component: LoanCalculatorComponent },
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
