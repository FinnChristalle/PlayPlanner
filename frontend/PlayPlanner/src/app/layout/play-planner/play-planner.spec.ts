import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlayPlanner } from './play-planner';

describe('PlayPlanner', () => {
  let component: PlayPlanner;
  let fixture: ComponentFixture<PlayPlanner>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PlayPlanner]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PlayPlanner);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
