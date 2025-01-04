import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../core/ApiService';
import { CardModule } from 'primeng/card'; 
import { ChartModule } from 'primeng/chart';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-dashboard',
  imports: [CardModule, ChartModule, CommonModule],
  templateUrl: './dashboard.component.html'
})
export class DashboardComponent implements OnInit {
  totalProducts = 0;
  totalCategories = 0;
  lowStockProducts: any[] = [];
  chartData: any;
  loading = false;

  constructor(private apiService: ApiService) { }

  ngOnInit(): void {
    this.fetchDashboardData();
  }

  fetchDashboardData(): void {
    this.loading = true;
    this.apiService.getProducts({ page: 1, pageSize: 100, sortOrder: 'asc' }).subscribe({
      next: (response) => {
        this.totalProducts = response.totalItems;
        this.lowStockProducts = response.products.filter((p: any) => p.stockQuantity < 10);
        this.updateChartData(response.products);
        this.loading = false;
      },
      error: (err) => {
        console.error('Failed to fetch products:', err);
        this.loading = false;
      }
    });

    this.apiService.getCategories().subscribe({
      next: (data) => {
        this.totalCategories = data.length;
      },
      error: (err) => {
        console.error('Failed to fetch categories:', err);
      }
    });
  }

  updateChartData(products: any[]): void {
    const categories = products.reduce((acc: any, product: any) => {
      acc[product.categoryName] = (acc[product.categoryName] || 0) + 1;
      return acc;
    }, {});

    this.chartData = {
      labels: Object.keys(categories),
      datasets: [
        {
          data: Object.values(categories),
          backgroundColor: ['#FF6384', '#36A2EB', '#FFCE56']
        }
      ]
    };
  }
}
