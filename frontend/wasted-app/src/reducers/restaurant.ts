import {createReducer} from "@reduxjs/toolkit"
import {logOutRestaurant, setRestaurantId} from "../actions/restaurant"

interface State {
  id: string
}

export const initialState: State = {
  id: ""
}

export const restaurant = createReducer(initialState, builder => {
  builder.addCase(setRestaurantId, (state, {payload}) => {
    state.id = payload.id
  })
  builder.addCase(logOutRestaurant, state => {
    state.id = initialState.id
  })
})
