import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

// Feature Components
import { ProductsComponent } from './features/products/products.component';
import { CategoriesComponent } from './features/categories/categories.component';
import { DashboardComponent } from './features/dashboard/dashboard.component';

// PrimeNG Modules
import { TableModule } from 'primeng/table'; // For p-table
import { CardModule } from 'primeng/card'; // For p-card
import { TreeModule } from 'primeng/tree'; // For p-tree
import { ButtonModule } from 'primeng/button'; // For action buttons in p-table
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { MockInterceptor } from './core/mock-interceptor';
import { ChartModule } from 'primeng/chart';

@NgModule({
  declarations: [
    AppComponent,
    ProductsComponent,
    CategoriesComponent,
    DashboardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    TableModule,
    CardModule,
    TreeModule,
    ButtonModule,
    HttpClientModule,
    ChartModule // Import ChartModule here
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: MockInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
