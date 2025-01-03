import { Component } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html'
})
export class DashboardComponent {
  totalProducts = 10; // Mock value, replace with actual data when API is ready
  totalCategories = 5; // Mock value, replace with actual data when API is ready

  chartData = {
    labels: ['Electronics', 'Clothing'],
    datasets: [
      {
        data: [6, 4],
        backgroundColor: ['#42A5F5', '#66BB6A']
      }
    ]
  };
}
