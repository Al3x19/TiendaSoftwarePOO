import { createContext, useState, useEffect } from "react";

// crear el contexto
export const createContext = createContext();

const cardProvider = ({ children }) => {
  const [cart, setCart] = useState([]);

  useEffect(() => {
    const total = cart.reduce((accumulator, currentItem) => {
      return accumulator + currentItem.price * currentItem.amount;
    }, 0);
    setTotal(total);
  });

  // actualizar cantidad del item
  useEffect(() => {
    if (cart) {
      const amount = cart.reduce((accumulator, currentItem) => {
        return accumulator + currentItem.amount;
      }, 0);
      setItemAmount(amount);
    }
  }, [cart]);

  //agregar al carrito

  const addToCart = (product, id) => {
    const newItem = { ...product, amount: 1 };
    // comprobar si el articulo ya esta en el carrito
    const cartItem = cart.find((item) => item.id === id);
    // si el artículo del carrito ya está en el carrito
    if (cartItem) {
      const newCart = [...cart].map((item) => {
        if (item.id === id) {
          return { ...item, amount: cartItem.amount + 1 };
        } else {
          return item;
        }
      });
      setCart(newCart);
    } else {
      setCart(...cart, newItem);
    }
  };
  // quitar del carrito
  const removeFromCart = (id) => {
    const newCart = cart.filter((item) => item.id !== id);
    setCart(newCart);
  };

  // limpiar carrito
  const clearCart = () => {
    setCart([]);
  };
};

export default cardProvider;
