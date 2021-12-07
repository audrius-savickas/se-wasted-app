import React, {useEffect, useState} from "react"
import {ListRenderItemInfo} from "react-native"
import {Text, View} from "react-native-ui-lib"
import {getAllRestaurants} from "../../../api"
import {Restaurant, RestaurantSortType} from "../../../api/interfaces"
import {navigateToRestaurantInfo} from "../../../services/navigation"
import {HorizontalList} from "../../horizontal-list"
import {HorizontalListItem} from "../horizontal-list-item"
import {PopularRestaurantsProps} from "./interfaces"

export const PopularRestaurants = ({componentId, location}: PopularRestaurantsProps) => {
  const [restaurants, setRestaurants] = useState([] as Restaurant[])
  const [pageNumber, setPageNumber] = useState(1)

  const fetchRestaurants = async () => {
    setRestaurants(
      await getAllRestaurants({
        sortObject: {
          sortType: RestaurantSortType.NAME,
          coordinates: {longitude: location.longitude, latitude: location.latitude}
        }
      })
    )
  }

  const renderItem = ({item}: ListRenderItemInfo<Restaurant>) => (
    <HorizontalListItem
      name={item.name}
      imageURL={item.imageURL}
      onPress={() => navigateToRestaurantInfo(componentId, {componentId, restaurant: item})}
    />
  )

  const onEndReached = async () => {
    const newRestaurants = await getAllRestaurants({
      sortObject: {
        sortType: RestaurantSortType.NAME,
        coordinates: {longitude: location.longitude, latitude: location.latitude}
      },
      pagination: {
        pageNumber: pageNumber + 1,
        pageSize: 10
      }
    })

    if (newRestaurants.length) {
      setRestaurants(restaurants.concat(newRestaurants))
      setPageNumber(pageNumber + 1)
    }
  }

  useEffect(() => {
    fetchRestaurants()
  }, [])

  return (
    <View centerV margin-s4>
      <Text text50L marginB-s2>
        ⭐️ Popular restaurants
      </Text>
      <HorizontalList items={restaurants} renderItem={renderItem} onEndReached={onEndReached} />
    </View>
  )
}
