import {createAction, createAsyncThunk} from "@reduxjs/toolkit"
import {getRestaurantById} from "../api"
import {Restaurant} from "../api/interfaces"

export const setRestaurantId = createAction<{id: string}>("SET_RESTAURANT_ID")

export const logOutRestaurant = createAction("LOG_OUT_RESTAURANT")

export const getRestaurantFromId = createAsyncThunk<Restaurant, {restaurantId: string}>(
  "GET_RESTAURANT_BY_ID",
  async ({restaurantId}) => {
    return await getRestaurantById({idRestaurant: restaurantId})
  }
)

export const updateRestaurant = createAction<Partial<Restaurant>>("UPDATE_RESTAURANT")
