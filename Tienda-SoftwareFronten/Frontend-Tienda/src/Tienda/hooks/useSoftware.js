import { useState} from "react";
import { getSoftwareList, getSoftware } from "../../shared/actions/software/Software";

export const useSoftware = () => {
  const [Software, setSoftware] = useState(null);
  const [isLoading, setIsLoading] = useState(false);

  const loadSoftware = async (searchTerm, page) => {
    setIsLoading(true);
    const result = await getSoftwareList(searchTerm, page);
    setSoftware(result);
    setIsLoading(false);
  };

  const loadSingleSoftware = async (id) => {
    const result = await getSoftware(id);
    setSoftware(result);
  
  };

  return {
    Software,
    isLoading,
    loadSoftware,
    loadSingleSoftware
  };
};
