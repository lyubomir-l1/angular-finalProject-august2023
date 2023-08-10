import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
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
    IncomesComponent,
    ExpensesComponent,
    CategoriesComponent,
    MonthlyReportComponent,
    AnnualReportComponent,
    LoanCalculatorComponent,
    FilterComponent,
    InputComponent   
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'incomes', component: IncomesComponent, canActivate: [AuthorizeGuard] },
      { path: 'expenses', component: ExpensesComponent, canActivate: [AuthorizeGuard] },
      { path: 'categories', component: CategoriesComponent, canActivate: [AuthorizeGuard] },
      { path: 'monthly-report', component: MonthlyReportComponent, canActivate: [AuthorizeGuard] },
      { path: 'anual-report', component: AnnualReportComponent, canActivate: [AuthorizeGuard] },
      { path: 'loan-calculator', component: LoanCalculatorComponent, canActivate: [AuthorizeGuard] },
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
