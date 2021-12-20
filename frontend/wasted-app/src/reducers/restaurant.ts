import {createReducer} from "@reduxjs/toolkit"
import {getRestaurantFromId, logOutRestaurant, setRestaurantId, updateRestaurant} from "../actions/restaurant"
import {Restaurant} from "../api/interfaces"

interface State {
  id: string
  restaurant: Restaurant
}

export const initialState: State = {
  id: "",
  restaurant: {} as Restaurant
}

export const restaurant = createReducer(initialState, builder => {
  builder.addCase(setRestaurantId, (state, {payload}) => {
    state.id = payload.id
  })
  builder.addCase(logOutRestaurant, state => {
    state.id = initialState.id
  })
  builder.addCase(getRestaurantFromId.fulfilled, (state, {payload}) => {
    state.restaurant = payload
  })
  builder.addCase(updateRestaurant, (state, {payload}) => {
    state.restaurant = {...state.restaurant, ...payload}
  })
})
