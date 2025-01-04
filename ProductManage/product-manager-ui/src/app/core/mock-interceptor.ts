import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';
import { Observable, of } from 'rxjs';

@Injectable()
export class MockInterceptor implements HttpInterceptor {
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (req.url.includes('/api/products')) {
      return of(new HttpResponse({
        status: 200,
        body: [
          { id: 1, name: 'Laptop', price: 1000 },
          { id: 2, name: 'Shirt', price: 50 }
        ]
      }));
    }
    if (req.url === '/api/categories') {
      return of(new HttpResponse({
        status: 200,
        body: [
          { id: 1, name: 'Electronics', description: 'All electronic items' },
          { id: 2, name: 'Clothing', description: 'Men and Women clothing' }
        ]
      }));
    }
    return next.handle(req);
  }
}
