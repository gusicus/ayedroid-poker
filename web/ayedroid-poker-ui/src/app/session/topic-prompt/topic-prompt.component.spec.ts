import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TopicPromptComponent } from './topic-prompt.component';

describe('TopicPromptComponent', () => {
  let component: TopicPromptComponent;
  let fixture: ComponentFixture<TopicPromptComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TopicPromptComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TopicPromptComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
