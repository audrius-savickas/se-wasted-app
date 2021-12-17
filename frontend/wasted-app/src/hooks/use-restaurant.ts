import {useDispatch, useSelector} from "react-redux"
import {getRestaurantFromId, logOutRestaurant, setRestaurantId, updateRestaurant} from "../actions/restaurant"
import {Restaurant} from "../api/interfaces"
import {getRestaurant, getRestaurantId} from "../selectors/restaurant"

export const useRestaurant = () => {
  const dispatch = useDispatch()

  return {
    restaurantId: useSelector(getRestaurantId),
    restaurant: useSelector(getRestaurant),
    getRestaurantFromId: ({restaurantId}: {restaurantId: string}) => dispatch(getRestaurantFromId({restaurantId})),
    setRestaurantId: (restaurantId: string) => dispatch(setRestaurantId({id: restaurantId})),
    logOutRestaurant: () => dispatch(logOutRestaurant()),
    updateRestaurant: (restaurant: Partial<Restaurant>) => dispatch(updateRestaurant(restaurant))
  }
}
