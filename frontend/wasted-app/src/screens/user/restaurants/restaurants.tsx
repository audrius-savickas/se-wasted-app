import React, {useEffect, useState} from "react"
import {Navigation} from "react-native-navigation"
import {Button, Colors, Incubator, LoaderScreen, View} from "react-native-ui-lib"
import {getAllRestaurants} from "../../../api"
import {Restaurant} from "../../../api/interfaces"
import {RestaurantsList} from "../../../components/restaurants-list"
import {setHomeRoot} from "../../../services/navigation"
import {HOME_BUTTON} from "../home-button"
import {RestaurantListProps} from "./interfaces"

export const RestaurantList = ({componentId}: RestaurantListProps) => {
  const [restaurants, setRestaurants] = useState([] as Restaurant[])
  const [renderedRestaurants, setRenderedRestaurants] = useState([] as Restaurant[])
  const [loading, setLoading] = useState(true)
  const [searchValue, setSearchValue] = useState("")

  const fetchRestaurants = async () => {
    const response = await getAllRestaurants()
    setRestaurants(response)
    setRenderedRestaurants(response)
    setLoading(false)
  }

  const search = () => {
    const filteredRestaurants = restaurants.filter(restaurant => {
      if (restaurant.name.toLowerCase().includes(searchValue.toLowerCase())) {
        return restaurant
      }
    })
    setRenderedRestaurants(filteredRestaurants)
  }

  useEffect(() => {
    Navigation.mergeOptions(componentId, {topBar: {leftButtons: [HOME_BUTTON]}})
    const listener = Navigation.events().registerNavigationButtonPressedListener(({buttonId}) => {
      if (buttonId === "GO_BACK") {
        setHomeRoot()
      }
    })

    fetchRestaurants()

    return () => listener.remove()
  }, [])

  return (
    <View flex>
      {loading ? (
        <LoaderScreen color={Colors.blue30} message="Loading..." />
      ) : (
        <>
          <View marginH-s8 marginT-s4 marginB-s3>
            <View row br20 padding-s3style={{borderWidth: 1, borderColor: Colors.grey30}}>
              <View flexG>
                <Incubator.TextField
                  text70
                  label="Name"
                  value={searchValue}
                  labelStyle={{marginBottom: 4}}
                  placeholder="Name of restaurant"
                  fieldStyle={{borderBottomWidth: 1, borderColor: Colors.purple40, paddingBottom: 4}}
                  onChangeText={text => setSearchValue(text)}
                />
              </View>
              <Button
                bg-transparent
                marginR-s3
                iconStyle={{width: 21, height: 21}}
                iconSource={require("../../../../assets/control.png")}
              />
              <Button
                bg-transparent
                marginR-s1
                iconStyle={{width: 21, height: 21}}
                iconSource={require("../../../../assets/search.png")}
                onPress={() => search()}
              />
            </View>
          </View>
          <RestaurantsList componentId={componentId} restaurants={renderedRestaurants} />
        </>
      )}
    </View>
  )
}
