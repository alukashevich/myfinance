import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './core/nav-menu/nav-menu.component';
import { HomeComponent } from './pages';
import { CounterComponent } from './pages';
import { FetchDataComponent } from './pages';
import { RegisterComponent } from './pages';
import { LoginComponent } from './pages';
import { CreditComponent } from './pages';
import { MonthPaymentComponent } from './components';

import { HttpService } from './core/services';
import { DataService } from './core/services';
import { AccountService } from './core/services';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    RegisterComponent,
    LoginComponent,
    CounterComponent,
    FetchDataComponent,
    CreditComponent,
    MonthPaymentComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    HttpModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: CreditComponent, pathMatch: 'full' },
      { path: 'register', component: RegisterComponent, pathMatch: 'full' },
      { path: 'login', component: LoginComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
    ])
  ],
  providers: [    
    HttpService,
    DataService,
    AccountService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
