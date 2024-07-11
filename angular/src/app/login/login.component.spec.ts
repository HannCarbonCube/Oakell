import { ComponentFixture, TestBed } from '@angular/core/testing';
import { LoginComponent } from './login.component';

// This is a custom login component and not the component currently in use on the frontend.
// This component will be used later on when custom login screen gets built
// Destroy this comments when that happens
describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LoginComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
