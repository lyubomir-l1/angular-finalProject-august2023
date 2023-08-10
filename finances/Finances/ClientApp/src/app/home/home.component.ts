import { Component } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  age: number = 0;
  income: number = 0;
  savings: number = 0;
  advises: any = {
    default: "Please insert your age, monthly income and monthly savings, then click on \"Get Advise\" button.",
    bad: "You savigs are under the target of [TARGET] for your age. You have to check your unfixed costs and/or grow your income.",
    good: "Good performance! Your savings are [TARGET] witch is more than target for your age. Check options for generate more passive incomes with your free money.",
    invalid: "Please insert valid age, income and savings.",
    invalidSavings: "Your savings cannot be more than your income! Please insert correct savings.",
    invalidAge: "Your age should be a whole number!"
  }
  message: string = this.advises.default;
  messageColor: string = '#3261A4';

  getAdvise = () => {
    const isValidAge = this.age >= 0;
    const isValidIncome = this.income >= 0;
    const isValidSavings = this.savings >= 0;

    if (isValidAge && isValidIncome && isValidSavings) {
      let savingsPer: number = 0;

      if (this.income > 0) {
        savingsPer = Number(((this.savings / this.income) * 100).toFixed(0));
      }
      console.log(savingsPer);

      let target = this.getSavingsTarget();

      if (Number.isInteger(+this.age) === false) {
        this.message = this.advises.invalidAge;
        this.messageColor = '#C82333';

      } else if (savingsPer > 100) {

        this.message = this.advises.invalidSavings;
        this.messageColor = '#C82333';
      }
      else if (savingsPer < target) {
        this.message = this.advises.bad.replace("[TARGET]", `${target}%`);
        this.messageColor = '#E0A800';
      } else {
        this.message = this.advises.good.replace("[TARGET]", `${savingsPer}%`);
        this.messageColor = '#218838';
      }
    } else {
      this.message = this.advises.invalid;
      this.messageColor = '#C82333';
    }
  }

  resetState() {
    this.age = 0;
    this.income = 0;
    this.savings = 0;
    this.message = this.advises.default;
    this.messageColor = '#3261A4';
  }

  getSavingsTarget = () => {
    let target = 0;

    if (this.age <= 30) {
      target = 10;
    } else if (this.age <= 40) {
      target = 15;
    } else {
      target = 20;
    }

    return target;
  }
}
